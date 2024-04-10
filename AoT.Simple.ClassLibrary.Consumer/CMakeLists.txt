cmake_minimum_required(VERSION 3.27)
project(AoT_Simple_ClassLibrary_Consumer)

set(CMAKE_CXX_STANDARD 17)

add_executable(AoT_Simple_ClassLibrary_Consumer main.cpp)

file(COPY dist DESTINATION ${CMAKE_BINARY_DIR})