#include "src/RobkoLib/RobkoLib.h"

Robko robko;

void setup()
{
  robko.init();
}

void loop()
{
	//robko.reciveCommand();
	robko.doTask();
	//robko.transmitReply();
}
