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
    for (byte i = 0; i < DATA_LENGTH; i++)
    {
        frame_[i] = _streamRef->read();
    }
}

bool BusController::Validate_frame()
{
    return frameFormer_.Validate_frame(frame_, DATA_LENGTH);
}
void BusController::getOpCode()
{
    opCode = frameFormer_.getOpCode_Fromframe(frame_);
}
void BusController::getPayload()
{
    frameFormer_.getPayload(frame_, payload_, PAYLOAD_LENGTH);
}

void BusController::getAbsolute_Angles_q1q2q3(float *q)
{
    frameFormer_.getAbsolute_Angles_q1q2q3_FromPayload(payload_, q);
}

void BusController::setSerial(Stream *streamObject)
{
    _streamRef = streamObject;
}

void BusController::SendResponse_Done()
{
    frameFormer_.DoResponseFrame_Done(frameResponse, RESPONSE_FRAME_LENGTH);
    _streamRef->write(frameResponse, RESPONSE_FRAME_LENGTH);
}
void BusController::SendError()
{
    frameFormer_.DoResponseFrame_Error(frameResponse, RESPONSE_FRAME_LENGTH);
    _streamRef->write(frameResponse, RESPONSE_FRAME_LENGTH);
    // _streamRef->write(66); //--
}