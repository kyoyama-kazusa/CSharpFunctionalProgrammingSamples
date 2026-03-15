#pragma once

// 使用 stdcall 调用约定的函数
__declspec(dllexport) int __stdcall stdcall_add(int a, int b);

// 使用 cdecl 调用约定的函数  
__declspec(dllexport) int __cdecl cdecl_add(int a, int b);

// 变参函数的 cdecl 示例（只有 cdecl 支持变长参数）
__declspec(dllexport) int __cdecl vararg_sum(int count, ...);
