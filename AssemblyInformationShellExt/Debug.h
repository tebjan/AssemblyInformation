// Debug.h - Local stub replacing external dependency
// Maps debug macros to ATLTRACE

#pragma once

#define TRACE_FUNCTION(x) ATLTRACE(_T("ENTER: %s\n"), x)

inline void Debug(int, LPCTSTR fmt, ...)
{
    // No-op in release, ATLTRACE in debug
#ifdef _DEBUG
    va_list args;
    va_start(args, fmt);
    TCHAR buf[1024];
    _vstprintf_s(buf, _countof(buf), fmt, args);
    va_end(args);
    ATLTRACE(_T("%s\n"), buf);
#else
    UNREFERENCED_PARAMETER(fmt);
#endif
}
