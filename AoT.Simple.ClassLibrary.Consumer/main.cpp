#ifdef _WIN32
#include "windows.h"
#define symLoad GetProcAddress
#else
#include "dlfcn.h"
#include <unistd.h>
#define symLoad dlsym
#endif

#include <iostream>

#define symLoad GetProcAddress

typedef unsigned long long(*gcd_func)(unsigned long long a, unsigned long long b);
typedef unsigned long long(*lcm_func)(unsigned long long a, unsigned long long b);

int main() {
    const LPCSTR path = "./dist/AoT.Simple.ClassLibrary.dll";
    const LPCSTR gcd_entry_point = "csharp_maths_gcd";
    const LPCSTR lcm_entry_point = "csharp_maths_lcm";

#ifdef _WIN32
    HMODULE handle = LoadLibraryA(path);
#else
    void *handle = dlopen(path, RTLD_LAZY);
#endif

    gcd_func gcd = reinterpret_cast<gcd_func>(symLoad(handle, gcd_entry_point));
    lcm_func lcm = reinterpret_cast<lcm_func>(symLoad(handle, lcm_entry_point));

    unsigned long long a;
    std::cout << "Enter the first number:" << std::endl;
    std::cin >> a;

    unsigned long long b;
    std::cout << "Enter the second number:" << std::endl;
    std::cin >> b;

    // gcd(24826148, 45296490) = 526
    // lcm(24826148, 45296490) = 2137903735020
    unsigned long long gcd_result = gcd(a, b);
    unsigned long long lcm_result = lcm(a, b);

    std::cout << "The greatest common divisor of " << a << " and " << b << " is: " << gcd_result << std::endl;
    std::cout << "The least common multiple of " << a << " and " << b << " is: " << lcm_result << std::endl;

    return 0;
}
