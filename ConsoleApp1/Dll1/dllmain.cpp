// dllmain.cpp : Defines the entry point for the DLL application.
#include "pch.h"

BOOL APIENTRY DllMain( HMODULE hModule,
                       DWORD  ul_reason_for_call,
                       LPVOID lpReserved
                     )
{
    switch (ul_reason_for_call)
    {
    case DLL_PROCESS_ATTACH:
    case DLL_THREAD_ATTACH:
    case DLL_THREAD_DETACH:
    case DLL_PROCESS_DETACH:
        break;
    }
    return TRUE;
}
#pragma region Test1: Send char array

typedef void(*setStringValuesCB_t)(char* pStringValues, int nValues);

static setStringValuesCB_t gSetStringValuesCB;

void NativeCallDelegate(char* pStringValues, int nValues)
{
    if (gSetStringValuesCB)
        gSetStringValuesCB(pStringValues, nValues);
}

extern "C" __declspec(dllexport)  void NativeLibCall(setStringValuesCB_t callback)
{
    gSetStringValuesCB = callback;
    char value[] = "One";
    //char* Values[] = { "One", "Two", "Three" };
    NativeCallDelegate(value, 3);
}
#pragma endregion

#pragma region Test2:

//unsigned char* ch;// = { 20,30,20 };
extern "C" __declspec (dllexport) unsigned char* ReadDataFileCPP(int& length)
{
    length = 3;
    unsigned char* ch = new unsigned char[length];
    for (int i = 0; i < length; i++)
        ch[i] = i * 30;
    return ch;
}
#pragma endregion


typedef void(*setBufferDataCB_t)(unsigned char* pStringValues, int nValues);

static setBufferDataCB_t gSetBufferDataCB;
extern "C" __declspec(dllexport)  void NativeLibCallBuffer(setBufferDataCB_t callback)
{
    gSetBufferDataCB = callback;

    int length = 3;
    unsigned char* ch = new unsigned char[length];
    for (int i = 0; i < length; i++)
        ch[i] = i * 30;
    callback(ch, length);
}