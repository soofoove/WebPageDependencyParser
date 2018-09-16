using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace WebPageDependencyParser
{
    public class Parser
    {
        private string _url;
        private TreeNode<string> _root;
        public Parser(string url)
        {
            _url = url;
            _root = new TreeNode<string>(url);
        }

        public async Task<string> LoadPageAsync(string url)
        {
            HttpWebRequest request = WebRequest.CreateHttp(url);
            WebResponse response = request.GetResponse();

            StreamReader sr = new StreamReader(response.GetResponseStream());

            return await sr.ReadToEndAsync();
        }

        public List<string> GetATags(string page)
        {
            List<string> tags = new List<string>();
            int a = page.IndexOf("<a ", 0);
            int end = 0;
            while (a > 0)
            {
                end = page.IndexOf(">", a);

                tags.Add(page.Substring(a, end - a + 1));

                a = page.IndexOf("<a ", end);
            }

            return tags;
        }

        public List<string> GetHrefsFromTags(List<string> tags)
        {
            List<string> hrefs = new List<string>();

            foreach (var tag in tags)
            {
                int index = tag.IndexOf("href=");
                if (index > 0)
                {
                    index = index + "href=\"".Length;
                    int end = tag.IndexOf("\"", index);
                    string href = tag.Substring(index, end - index);

                    if (href.IndexOf("http") >= 0)
                        hrefs.Add(href);
                }
            }
            return hrefs;
        }

        public TreeNode<string> Analyze(int depth)
        {
            analyze_proc(_url, depth, _root);
            return _root;
        }

        private void analyze_proc(string url, int depth, TreeNode<string> parent)
        {
            if (depth == 0)
            {
                parent.AddChild(new TreeNode<string>(url));
                return;
            }

            Task<string> t = LoadPageAsync(url);
            t.Wait();
            string page = t.Result;
            List<string> tags = GetATags(page);
            List<string> hrefs = GetHrefsFromTags(tags);

            TreeNode<string> node = new TreeNode<string>(url);
            if (node.Value == _root.Value)
                _root = node;
            else
                parent.AddChild(node);
            foreach (var href in hrefs)
            {
                if (!_root.IsContain(href))
                {
                    analyze_proc(href, depth - 1, node);
                }
            }
        }

    }
}