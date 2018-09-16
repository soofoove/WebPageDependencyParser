using System;
using System.IO;
using System.Threading.Tasks;

namespace WebPageDependencyParser
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length == 2)
                StartWork(args[0], int.Parse(args[1]));
            else
            {
                System.Console.WriteLine("USING: parser [url] [depth]");
            }
        }

        public static void StartWork(string url, int depth){
            Parser parser = new Parser(url);
            var tree = parser.Analyze(depth);
            TreeNode<string>.Print(tree);
            
        }

        public static void TreeTest(){
            System.Console.WriteLine("Test tree");
            TreeNode<string> root = new TreeNode<string>("1");
            TreeNode<string> child1 = new TreeNode<string>("2");
            TreeNode<string> child2 = new TreeNode<string>("3");
            TreeNode<string> child3 = new TreeNode<string>("4");
            TreeNode<string> child4 = new TreeNode<string>("6");

            root.AddChild(child1);
            root.AddChild(child2);
            child2.AddChild(child3);
            child3.AddChild(child4);

            System.Console.WriteLine("Tree filled");
            TreeNode<string>.Print(root);
        }
    }
}
