#makefile for main.cpp

debug: CXXFLAGS += -g -DDEBUG
debug: default

default: main.cpp
	${CXX} ${CXXFLAGS} -o merge main.cpp

clean:
	rm -f merge
