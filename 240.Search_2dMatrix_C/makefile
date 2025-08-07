CCFLAGS = -g3 -Wall

EXE = searchmatrix

all: $(EXE)

debug: CCFLAGS += -DDEBUG -g
debug: EXE = searchmatrix-dbg
debug: all

$(EXE): main.c
	$(CC) $(CCFLAGS) main.c -o $(EXE)

clean:
	rm -rf searchmatrix searchmatrix-dbg

