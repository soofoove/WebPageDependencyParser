using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace WebPageDependencyParser{
    public class Parser{
        private string _url;
        public Parser(string url){
            _url = url;
        }

        public async Task<string> LoadPageAsync(string url){
            WebRequest request = WebRequest.CreateHttp(url);
            WebResponse response = request.GetResponse();

            StreamReader sr = new StreamReader(response.GetResponseStream());

            return await sr.ReadToEndAsync();
        }

        public string GetNextTagA(string page, int pos){
            

            return null;
        }
    }
}