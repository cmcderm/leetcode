#include <iostream>
#include <vector>
#include <queue>

struct ListNode {
    int val;
    struct ListNode* next;
};

class Solution {
public:
    ListNode* mergeKLists(std::vector<ListNode*>& lists) {
        struct CompareNodes {
            bool operator()(ListNode* a, ListNode* b) const {
                return a->val > b->val; // min-heap by value
            }
        };

        std::priority_queue<ListNode*, std::vector<ListNode*>, CompareNodes> min_heap;

        for (auto head : lists) {
            if (head != nullptr) {
                min_heap.push(head);
            }
        }

        if (min_heap.empty()) {
            return nullptr;
        }

        ListNode* dummy = new ListNode{0, nullptr};
        ListNode* tail = dummy;

        while (!min_heap.empty()) {
            ListNode* node = min_heap.top();
            min_heap.pop();

            tail->next = node;
            tail = node;

            if (node->next != nullptr) {
                min_heap.push(node->next);
            }
        }

        return dummy->next;
    }
};

int main() {
    ListNode* l1 = new ListNode{1, new ListNode{4, new ListNode{5, nullptr}}};
    ListNode* l2 = new ListNode{1, new ListNode{3, new ListNode{4, nullptr}}};
    ListNode* l3 = new ListNode{2, new ListNode{6, nullptr}};
    std::vector<ListNode*> lists = {l1, l2, l3};

    Solution solution;
    ListNode* result = solution.mergeKLists(lists);

    // Print merged list
    ListNode* current = result;
    while (current != nullptr) {
        std::cout << current->val << " -> ";
        current = current->next;
    }
    std::cout << "nullptr" << std::endl;

    // Cleanup memory
    while (result != nullptr) {
        ListNode* temp = result;
        result = result->next;
        delete temp;
    }

    return 0;
}
