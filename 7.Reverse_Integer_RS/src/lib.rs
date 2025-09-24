

pub fn reverse(x: i32) -> i32 {
    let mut num = x;
    let mut result: i32 = 0;

    while num != 0 {
        let digit = num % 10;
        num /= 10;

        if result > i32::MAX / 10 || result < i32::MIN / 10 {
            return 0;
        }

        result = result * 10 + digit;
    }

    result
}

#[cfg(test)]
mod tests {
    use super::*;

    #[test]
    pub fn one() {
        let res = reverse(1);

        assert_eq!(1, res);
    }

    #[test]
    pub fn one_two_three() {
        let res = reverse(123);

        assert_eq!(321, res);
    }

    #[test]
    pub fn ten() {
        let res = reverse(10);

        assert_eq!(1, res);
    }

    #[test]
    pub fn negative() {
        let res = reverse(-123);

        assert_eq!(-321, res);
    }

    #[test]
    pub fn overflow() {
        let res = reverse(i32::MAX);

        assert_eq!(res, 0);
    }
}
