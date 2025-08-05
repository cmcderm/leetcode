struct MyLinkedList {
    head: Option<Box<Node>>,
    tail: Option<Box<Node>>,
}

struct Node {
    val: i32,
    prev: Option<Box<Node>>,
    next: Option<Box<Node>>,
}

impl Node {
    fn new(val: i32) -> Self {
        Node { val, prev: None, next: None }
    }
}

/**
 * `&self` means the method takes an immutable reference.
 * If you need a mutable reference, change it to `&mut self` instead.
 */
impl MyLinkedList {

    fn new() -> Self {
        MyLinkedList {
            head: None,
            tail: None
        }
    }

    fn get(&self, index: i32) -> Option<i32> {
        let mut count = 0;

        let mut result = None;

        while let Some(node) = self.head {
            if count == index {
                result = Some(node.val);
                break;
            } else {
                node = node.next?;
                count += 1;
            }

        };

        result
    }

    fn add_at_head(&self, val: i32) {

    }

    fn add_at_tail(&self, val: i32) {

    }

    fn add_at_index(&self, index: i32, val: i32) {

    }

    fn delete_at_index(&self, index: i32) {

    }
}

pub fn main() {
    let obj = MyLinkedList::new();



    let ret_1: Option<i32> = obj.get(3);
    assert!(ret_1 == None);

    // obj.add_at_head(val);
    // obj.add_at_tail(val);
    // obj.add_at_index(index, val);
    // obj.delete_at_index(index);
}




// Your MyLinkedList object will be instantiated and called as such:
// let obj = MyLinkedList::new();
// let ret_1: i32 = obj.get(index);
// obj.add_at_head(val);
// obj.add_at_tail(val);
// obj.add_at_index(index, val);
// obj.delete_at_index(index);
//
