// AssemblyInformation.cpp : Implementation of DLL Exports.


#include "stdafx.h"
#include "resource.h"
#include "AssemblyInformation_i.h"
#include "atlbase.h"
#include "AILoader.h"
#include "Debug.h"

using namespace ATL;
class CComModule _AtlModule;

BEGIN_OBJECT_MAP(ObjectMap)
	OBJECT_ENTRY(CLSID_AILoader, CAILoader)
END_OBJECT_MAP()

HMODULE GetCurrentModule()
{
	HMODULE hModule = NULL;
	GetModuleHandleEx(GET_MODULE_HANDLE_EX_FLAG_FROM_ADDRESS, (LPCTSTR)GetCurrentModule, &hModule);

	return hModule;
}

#define ASSEMBLY_INFORMATION_EXE _T("AssemblyInformation.exe")

// DLL Entry Point
extern "C" BOOL WINAPI DllMain(HINSTANCE hInstance, DWORD dwReason, LPVOID lpReserved)
{
	TRACE_FUNCTION(_T("DllMain"));
	hInstance;
	if ( dwReason == DLL_PROCESS_ATTACH )
	{
		CAILoader::szAIAppPath[0] = 0;
		TCHAR szPath[_MAX_ENV];

		GetModuleFileName(GetCurrentModule(), szPath, countof(szPath));
		PathRemoveFileSpec ( szPath );

		PathCombine(CAILoader::szAIAppPath, szPath, ASSEMBLY_INFORMATION_EXE);
		Debug(0, _T("AI Launcher path is <%s>"), CAILoader::szAIAppPath);
		ATLTRACE("AI Launcher path is <%s>\n", CAILoader::szAIAppPath);

		_AtlModule.Init(ObjectMap, hInstance, &LIBID_AssemblyInformationLib);
		DisableThreadLibraryCalls(hInstance);
	}
	else if (dwReason == DLL_PROCESS_DETACH)
		_AtlModule.Term();

	return TRUE;    // ok
}
// Used to determine whether the DLL can be unloaded by OLE.
STDAPI DllCanUnloadNow(void)
{
		return _AtlModule.DllCanUnloadNow();
}

// Returns a class factory to create an object of the requested type.
STDAPI DllGetClassObject(REFCLSID rclsid, REFIID riid, LPVOID* ppv)
{
		return _AtlModule.DllGetClassObject(rclsid, riid, ppv);
}

// DllRegisterServer - Adds entries to the system registry.
STDAPI DllRegisterServer(void)
{
	// Add ourselves to the list of approved shell extensions.
	{
		CRegKey reg;
		LONG    lRet;

		lRet = reg.Open ( HKEY_LOCAL_MACHINE,
			_T("Software\\Microsoft\\Windows\\CurrentVersion\\Shell Extensions\\Approved"),
			KEY_SET_VALUE );

		if ( ERROR_SUCCESS != lRet )
			return E_ACCESSDENIED;

		lRet = reg.SetStringValue ( _T("DllRegShlExt extension"),
			_T("{8AB81E72-CB2F-11D3-8D3B-AC2F34F1FA3C}") );


		if ( ERROR_SUCCESS != lRet )
			return HRESULT_FROM_WIN32(lRet);
	}
	// registers object, typelib and all interfaces in typelib
	HRESULT hr = _AtlModule.DllRegisterServer(FALSE);
		return hr;
}

// DllUnregisterServer - Removes entries from the system registry.
STDAPI DllUnregisterServer(void)
{
	// Remove ourselves from the list of approved shell extensions.
	// Note that if we get an error along the way, I don't bail out since I want
	// to do the normal ATL unregistration stuff too.
	{
		CRegKey reg;
		LONG    lRet;

		lRet = reg.Open ( HKEY_LOCAL_MACHINE,
			_T("Software\\Microsoft\\Windows\\CurrentVersion\\Shell Extensions\\Approved"),
			KEY_SET_VALUE );

		if ( ERROR_SUCCESS == lRet )
			lRet = reg.DeleteValue ( _T("{8AB81E72-CB2F-11D3-8D3B-AC2F34F1FA3C}") );
	}

	HRESULT hr = _AtlModule.DllUnregisterServer(FALSE);
		return hr;
}
