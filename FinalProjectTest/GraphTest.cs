using System;
using FinalProject;
using Microsoft.VisualStudio.TestTools.UnitTesting;


namespace FinalProjectTest
{
    [TestClass]
    public class GraphTest
    {
        [TestMethod]
        public void BuildGraphTest_1()
        {
            //Arrange
            Graph graph = new Graph();
            //Act
            graph.buildGraph("Amsterdam", 1);
            //Assert
            Assert.AreEqual(1, graph.getMaxDepth());
            Assert.AreEqual("Amsterdam", graph.getGraphHead().name);
            Assert.AreEqual(658, graph.getArticles().Count);
        }

        [TestMethod]
        public void BuildGraphTest_2()
        {
            //Arrange
            Graph graph = new Graph();
            //Act
            graph.buildGraph("Kfar Menahem", 2);
            //Assert
            Assert.AreEqual(2, graph.getMaxDepth());
            Assert.AreEqual("Kfar Menahem", graph.getGraphHead().name);
            Assert.AreEqual(3196, graph.getArticles().Count);
        }

        [TestMethod]
        public void BuildGraphWithRedirectTest()
        {
            //Arrange
            Graph graph = new Graph();
            //Act
            graph.buildGraph("Kfar Menachem", 2);
            //Assert
            Assert.AreEqual("Kfar Menahem", graph.getGraphHead().name);
            Assert.AreEqual(3196, graph.getArticles().Count);
        }

        [TestMethod]
        public void GraphIOTest()
        {
            //Arrange
            Graph graph = new Graph();
            Graph other_graph = new Graph();
            //Act
            graph.buildGraph("Berlin", 1);
            graph.saveTreeToFile();
            other_graph.loadTreeFromFile(Const.RESULTS_DIR_PATH + graph.getMaxDepth() + " " + graph.getGraphHead().name + @".tree");
            //Assert
            Assert.AreEqual("Berlin", graph.getGraphHead().name);
            Assert.AreEqual(565, graph.getArticles().Count);
            Assert.AreEqual("Berlin", other_graph.getGraphHead().name);
            Assert.AreEqual(565, other_graph.getArticles().Count);
        }


    }
}
