

public class TreeNode {
    public int val;
    public TreeNode? left;
    public TreeNode? right;
    public TreeNode(int val, TreeNode? left, TreeNode? right) {
        this.val = val;
        this.left = left;
        this.right = right;
    }
}

public class Solution {
    void DebugIList(IList<int> list) {
        Console.Write("Debug: [");
        for (int i = 0; i < list.Count; i++) {
            Console.Write(list[i]);
            if (i+1 < list.Count) {
                Console.Write(", ");
            }
        }
        Console.WriteLine("]");
    }

    public IList<int> InorderTraversal(TreeNode root) {
        return dfs(root);
    }

    public IList<int> dfs(TreeNode node) {
        if (node == null) { return new List<int>(); }

        Console.WriteLine($"{node.val}");

        IList<int> list;
        if (node.left != null) {
            Console.WriteLine($"Going to left node");
            list = dfs(node.left);
        } else {
            list = new List<int>();
        }

        list.Add(node.val);

        DebugIList(list);

        if (node.right != null) {
            Console.WriteLine($"Going to right node");
            list = list.Concat(dfs(node.right)).ToList();
        }

        return list;
    }
}

namespace Leetcode {
    public class MainSol {
        public static int Main() {
            TreeNode root = new TreeNode(1,
                null,
                new TreeNode(3, null,
                    new TreeNode(2, null, null)
                )
            );

            Solution soln = new Solution();

            IList<int> result = soln.InorderTraversal(root);

            Console.Write("Output: [");
            for (int i = 0; i < result.Count; i++) {
                Console.Write(result[i]);
                if (i+1 < result.Count) {
                    Console.Write(", ");
                }
            }
            Console.WriteLine("]");

            Console.WriteLine("Expected: [1, 3, 2]");

            return 0;
        }
    }
}


