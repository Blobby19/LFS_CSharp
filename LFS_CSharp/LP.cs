using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LFS_CSharp
{
    class LP
    {
        public bool enable = true;
        public double setpoint = 0.0d;
        public double vitesse = 0.0d;
        public double kp = 0.0d;
        public double ki = 0.0d;
        public double kd = 0.0d;
        public double _out;
        public double smoothOut;

        public double max = 100.0d;
        public double min = 0.0d;

        public double bias = 0.0d;
        public double maxDelta = 0.0d;

        public bool direct = true;
        public int exTime = 10000;
        public long lastExecTime = 0L;

        public double errorSum = 0.0d;
        public double lastError = 0.0d;

        private long executeTime;
        private double kPkIconst;
        private double kpOld;
        private double kiOld;
        private double scaledMax;
        private double scaledMin;


        public LP(double kp, double ki, double kd)
        {
            this.kp = kp;
            this.ki = ki;
            this.kd = kd;
        }

        public void execute()
        {
            if (!this.enable) return;
            DateTime now = DateTime.Now;
            long deltaTime = now.Ticks - lastExecTime;
            lastExecTime = now.Ticks;

            if (kp == 0.0d)
                return;
            float deltaSecs = (float)deltaTime / (float)1000000;

            double error = setpoint - vitesse;

            if(ki != 0.0d)
            {
                double iError = deltaSecs * error;
                errorSum += iError;

                if (direct)
                {
                    if (-errorSum > scaledMax)
                        errorSum = -scaledMax;
                    else if (-errorSum < scaledMin)
                        errorSum = -scaledMin;
                }
                else
                {
                    if (errorSum > scaledMax)
                        errorSum = scaledMax;
                    else if (errorSum < scaledMin)
                        errorSum = scaledMin;
                }
            }

            
            double proportionalGain = error * kp;
            double integrativeGain = kp * ki * errorSum / 60.0f;
            double derivativeGain = kp * kd * (error - lastError) / deltaSecs;

            lastError = error;

            double pv = proportionalGain + integrativeGain + derivativeGain;

            if (direct)
                pv = -pv;

            if (ki == 0.0d)
                pv = pv + bias;

            if (pv > max)
                pv = max;
            else if (pv < min)
                pv = min;
            
            if(maxDelta != 0d)
            {
                if(_out > pv)
                {
                    if (_out - pv > maxDelta)
                        _out = computeSmooth(_out - maxDelta);
                        //_out = _out - maxDelta;
                        
                    else
                        _out = computeSmooth(pv);
                        //_out = pv;
                }
                else
                {
                    if (pv - _out > maxDelta)
                        _out = computeSmooth(_out + maxDelta);
                        //_out = _out + maxDelta;
                    else
                        _out = computeSmooth(pv);
                        //_out = pv;
                }
            }
            else
            {
                _out = computeSmooth(pv);
                //_out = pv;
            }
        }

        public double computeSmooth(double pv)
        {
            return smoothOut = smoothOut - (0.1 * (smoothOut - pv));
        }

        public void computeConstants()
        {
            executeTime = (long)exTime * 1000;  //convert exTime into ticks
            kPkIconst = kp * ki / 60.0f;
            if (kPkIconst == 0.0f)
                    {
                scaledMax = 0.0f;
                scaledMin = 0.0f;
            }
            else
            {
                scaledMax = max / kPkIconst;
                scaledMin = min / kPkIconst;
            }
        }

    }
}
