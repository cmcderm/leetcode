#include <stdio.h>
#include <stdbool.h>
#include <stdlib.h>

static const int MATRIX_SIZE = 5;
static const int MATRIX_COLSIZE = 5;

void matrix_print(int** matrix, int matrixSize, int matrixColSize) {
    for (int i = 0; i < matrixSize; i++) {
        for (int j = 0; j < matrixColSize; j++) {
            printf("%d ", matrix[i][j]);
        }
        printf("\n");
    }
}

bool searchMatrix(int** matrix, const int matrixSize, const int* matrixColSize, const int target){
    int left = 0;
    int right = matrixSize - 1;

    // I'm imagining a 2d binary sort, first searching vertically, then horizontally
    // Outer then inner, I don't think binary search benefits from locality caching, but it feels logical
    // Best I've got is O(log(n)log(m))
    while (left <= right) {
        int mid_row = (left + right) / 2;


        int mid_top = matrix[mid_row][0];
        int mid_bottom = matrix[mid_row][(*matrixColSize) - 1];

        if (target < mid_top) {
            right = mid_row - 1;
        } else if (target > mid_bottom){
            left = mid_row + 1;
        } else {
            int top = 0;
            int bottom = (*matrixColSize) - 1;

            // This inner search doesn't give us any information regarding which side to split
            while (top <= bottom) {
                int mid = (top + bottom) / 2;

                int mid_val = matrix[mid_row][mid];

                if (target < mid_val ) {
                    bottom = mid - 1;
                } else if (target > mid_val) {
                    top = mid + 1;
                } else {
                    return true;
                }
            }
        }
    }

    return false;
}

int main() {
    int ref_matrix[5][5] = {
        { 1,  4,  7, 11, 15},
        { 2,  5,  8, 12, 19},
        { 3,  6,  9, 16, 22},
        {10, 13, 14, 17, 24},
        {18, 21, 23, 26, 30}
    };

    int** matrix = malloc(MATRIX_SIZE * sizeof(int*));
    if (matrix == NULL) {
        perror("Outer malloc failed");
        exit(1);
    }

    for (int i = 0; i < MATRIX_SIZE; i++) {
        matrix[i] = malloc(MATRIX_COLSIZE * sizeof(int));

        for (int j = 0; j < MATRIX_COLSIZE; j++) {
            matrix[i][j] = ref_matrix[i][j];
        }
    }

    matrix_print(matrix, MATRIX_SIZE, MATRIX_COLSIZE);

    bool result = searchMatrix(matrix, MATRIX_SIZE, &MATRIX_COLSIZE, 5);

    if (result) {
        printf("Found target!\n");
    } else {
        printf("Not found??\n");
    }

    for (int i = 0; i < MATRIX_SIZE; ++i) {
        free(matrix[i]);
    }

    free(matrix);

    return 0;
}


