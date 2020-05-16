#include "BusController.h"

BusController::BusController()
{
    frameFormer_ = *(new FrameFormer);
}

BusController::~BusController()
{
}

void BusController::getFrame()
{
    // length_ = _streamRef->available();//??//---------
    // delete [] frame_;//??//------
    // *frame_ = *(new byte[length_]);//??//-----------
    for (byte i = 0; i < length_; i++)
    {
        frame_[i] = _streamRef->read();
    }
}

bool BusController::Validate_frame()
{
    return frameFormer_.Validate_frame(frame_, length_);
}


void BusController::getAbsolute_Angles_q1q2q3(float *q)
{
    frameFormer_.getAbsolute_Angles_q1q2q3_FromPayload(payload_, q);
}
float BusController::getGripperAbsoluteDistance() {
  return  frameFormer_.getGripperAbsoluteDistance_FromPayload(payload_);
}
void BusController::setSerial(Stream *streamObject)
{
    _streamRef = streamObject;
}

void BusController::SendResponse_Done()
{
    frameFormer_.DoResponseFrame_Done(frameResponse, RESPONSE_FRAME_LENGTH, opCode);
    _streamRef->write(frameResponse, RESPONSE_FRAME_LENGTH);
}
void BusController::SendResponse_Error()
{
    frameFormer_.DoResponseFrame_Error(frameResponse, RESPONSE_FRAME_LENGTH, opCode);
    _streamRef->write(frameResponse, RESPONSE_FRAME_LENGTH);
    // _streamRef->write(66); //--
}
void BusController::parse_frame() {     
    // length_ = frameFormer_.getLength_Fromframe(frame_);//------------
    opCode = frameFormer_.getOpCode_Fromframe(frame_);
    // delete [] payload_;//??//-----------
    // *payload_ = *(new byte[length_ - FRAME_LENGTH_WITHOUT_PAYLOAD]);//??//-----------
    frameFormer_.getPayload(frame_, payload_, length_ - FRAME_LENGTH_WITHOUT_PAYLOAD);
}

