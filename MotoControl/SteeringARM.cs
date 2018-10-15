using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;


namespace MotorControl
{

    //机械臂动作
    public class ARM_Movement
    {
        public double LowLimit = -100; //lowest
        public double HiLimit = 100;   //highest
        public double DefaultPos = 0;    //-100 ~ 100
        public double speed = 5;  //1-10
        public double pos = 0;

        public void Set(bool direction, double speed)
        {
            if (direction)
            {
                if (pos < HiLimit)
                {
                    pos = pos + 1;
                }
            }
            else
            {
                if (pos > LowLimit)
                {
                    pos = pos - 1;
                }
            }
        }

    }

    //机械臂分解关节姿态

    public class ARM6
    {
        public ARM6()
        {

        }    //姿态 （输入）
        public ARM_Movement Head_LR = new ARM_Movement(); //头部左转右转
        public ARM_Movement Head_UD = new ARM_Movement(); //低头抬头
        public ARM_Movement Head_SLR = new ARM_Movement(); //左歪右歪

        public ARM_Movement Body_LR = new ARM_Movement(); //左转右转
        public ARM_Movement Body_UD = new ARM_Movement(); //上下
        public ARM_Movement Body_FN = new ARM_Movement(); //前后

        
        //舵机控制 （输出）
        public SteeringControl HeadJoint_LR = new SteeringControl();
        public SteeringControl HeadJoint_UD = new SteeringControl();
        public SteeringControl HeadJoint_SLR = new SteeringControl();

        public SteeringControl BodyJoint_LR = new SteeringControl();
        public SteeringControl BodyJoint_UPPER = new SteeringControl();
        public SteeringControl BodyJoint_LOWER = new SteeringControl();

        public void Move_Head_LR (bool direction,double speed)
        {
            Head_LR.Set(direction, speed);
        }





    }
}