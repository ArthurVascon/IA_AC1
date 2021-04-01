using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public struct Link
{
    //OU VAI OU VAI E VOLTA
    public enum direction { UNI, BI }

    //PONTO A
    public GameObject node1;

    // PONTO B
    public GameObject node2;

    // A -> B OU A < - > B
    public direction dir;
}


public class WPManager : MonoBehaviour
{
    // PONTOS DO MAPA
    public GameObject[] waypoints;

    // LINHAS DE CAMINHO
    public Link[] links;
    public Graph graph = new Graph();

    
    void Start()
    {
        // AQUI VAI MONTAR O MAPINHA MAROTO COMO A GENTE VÊ LÁ COM AS LINHAS QUEM COM QUEM E SAS COISAS
        if (waypoints.Length > 0)
        {
            foreach (GameObject wp in waypoints)
            {
                graph.AddNode(wp);
            }
            foreach (Link l in links)
            {
                graph.AddEdge(l.node1, l.node2);
                if (l.dir == Link.direction.BI)
                    graph.AddEdge(l.node2, l.node1);
            }
        }
    }

    
    void Update()
    {
        //AOW, DESENHA AE PA NOIS
        graph.debugDraw();
    }
}
