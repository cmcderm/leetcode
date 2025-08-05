#include <stdio.h>
#include <stdbool.h>
#include <stdlib.h>

const int MATRIX_SIZE = 5;
const int MATRIX_COLSIZE = 5;

void matrix_print(int** matrix, int matrixSize, int matrixColSize) {
    for (int i = 0; i < matrixSize; i++) {
        for (int j = 0; j < matrixColSize; j++) {
            printf("%d ", matrix[i][j]);
        }
        printf("\n");
    }
}

bool searchMatrix(int** matrix, int matrixSize, int* matrixColSize, int target){
    int left = 0;
    int right = matrixSize;

    int column = -1;

    while (left < right) {
        int mid_column = (left + right) / 2;

        int mid_top = matrix[0][mid_column];
        int mid_bottom = matrix[matrixSize-1][mid_column];

        if (target < mid_top) {
            right = mid_column - 1;
        } else if (target > mid_bottom){
            left = mid_column + 1;
        } else {
            column = mid_column;
            break;
        }
    }

    if (column == -1) {
        return false;
    }

    left = 0;
    right = (*matrixColSize);

    while (left < right) {
        int mid = (left + right) / 2;

        int mid_value = matrix[mid][column];

        if (target < mid_value) {
            right = mid - 1;
        } else if (target > mid_value){
            left = mid + 1;
        } else {
            return true;
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

    int** matrix = malloc(MATRIX_SIZE * MATRIX_COLSIZE * sizeof(int*));

    for (int i = 0; i < MATRIX_SIZE; i++) {
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

    free(*matrix);

    return 0;
}


