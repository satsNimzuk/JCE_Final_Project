using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestProject
{
    class PageRankTester
    {
        public void test1()
        {
            Node n1 = new Node();
            n1.name = "1";

            Node n2 = new Node();
            n2.name = "2";

            List<Node> list = new List<Node>();
            list.Add(n2);

            n1.links = list;

            LinkMatrix lm = new LinkMatrix(n1);
            lm.calculateRank(60);

            Vector result = lm.getPageRank();
        }

        public void test2()
        {
            Node n1 = new Node();
            n1.name = "1";
            n1.depth = 6;
            Node n2 = new Node();
            n2.name = "2";
            n2.depth = 5;
            Node n3 = new Node();
            n3.name = "3";
            n3.depth = 4;
            Node n4 = new Node();
            n4.name = "4";
            n4.depth = 4;
            Node n5 = new Node();
            n5.name = "5";
            n5.depth = 3;
            Node n6 = new Node();
            n6.name = "6";
            n6.depth = 2;
            Node n7 = new Node();
            n7.name = "7";
            n7.depth = 0;
            Node n8 = new Node();
            n8.name = "8";
            n8.depth = 1;

            List<Node> n1_links = new List<Node>();
            n1_links.Add(n2);
            n1_links.Add(n3);

            List<Node> n2_links = new List<Node>();
            n2_links.Add(n4);

            List<Node> n3_links = new List<Node>();
            n3_links.Add(n2);
            n3_links.Add(n5);

            List<Node> n4_links = new List<Node>();
            n4_links.Add(n2);
            n4_links.Add(n5);
            n4_links.Add(n6);

            List<Node> n5_links = new List<Node>();
            n5_links.Add(n6);
            n5_links.Add(n7);
            n5_links.Add(n8);

            List<Node> n6_links = new List<Node>();
            n6_links.Add(n8);

            List<Node> n7_links = new List<Node>();
            n7_links.Add(n1);
            n7_links.Add(n5);
            n7_links.Add(n8);

            List<Node> n8_links = new List<Node>();
            n8_links.Add(n6);
            n8_links.Add(n7);

            n1.links = n1_links;
            n2.links = n2_links;
            n3.links = n3_links;
            n4.links = n4_links;
            n5.links = n5_links;
            n6.links = n6_links;
            n7.links = n7_links;
            n8.links = n8_links;

            LinkMatrix lm = new LinkMatrix(n1);
            lm.calculateRank(60);

            Vector result = lm.getPageRank();           
        }



    }
}
