using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using StandartMainForm;
using MCControl;
using InverseKinematics;

namespace CommandSend
{
    /*Класс для отправки списка команд*/
    class CommandSender
    {
        private string[] commandList;
        public string[] CommandList { get { return commandList; } set { commandList = value; } }//задание в котором есть G коды и свои коды

        private double intrStep;
        public double IntrStep { get { return intrStep; } set { intrStep = value; } } //шаг интерполяции в мм

        private MainForm owner;
        public MainForm Owner { get { return owner; } set { owner = value; } }

        private MCController mCController;
        public MCController MCController { get { return mCController; } set { mCController = value; } } //для приема и отправки пакета данных 

        private IKSolver3DOF iKSolver3DOF;
        public IKSolver3DOF IKSolver3DOF { get { return iKSolver3DOF; } set { iKSolver3DOF = value; } }


        /*-----------------------------------реализация-------------------------------------------*/


        public CommandSender()
        {

        }
        ~CommandSender()
        {

        }

        public void Start()
        {

        }

    }
}
