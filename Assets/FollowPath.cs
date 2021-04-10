using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPath : MonoBehaviour
{
    //PONTO OBJETIVO
    Transform goal;

    //VELO DO TANK
    float speed = 5.0f;

    //AFASTA DO PONTO FINAL
    float accuracy = 1.0f;

    //VELOCIDADE DE ROTAÇÃO DO TANK
    float rotSpeed = 2.0f;

    //WPMANAGER
    public GameObject wpManager;

    //PONTOS DO MAPA
    GameObject[] wps;
    //PONTO ATUAL
    GameObject currentNode;
    int currentWP = 0;
    //DESENHO
    Graph g;


    void Start()
    {
        //PEGA O WAYPOINTS E O GRAPH DO WPMANAGER
        wps = wpManager.GetComponent<WPManager>().waypoints;
        g = wpManager.GetComponent<WPManager>().graph;

        //VALOR 0 INICIAL
        currentNode = wps[0];
    }


    private void LateUpdate()
    {
        //SE FOR 0, TCHAU
        if (g.getPathLength() == 0 || currentWP == g.getPathLength())
            return;

        //O PONTO MAIS PRÓXIMO VAI SER ESSE DAQUI
        currentNode = g.getPathPoint(currentWP);

        //SE TIVER MUITO PERTO ELE VAI PRO PRÓXIMO, MAS DEPENDE...
        if (Vector3.Distance(g.getPathPoint(currentWP).transform.position, transform.position) < accuracy)
        {
            currentWP++;
        }

        //ELE VAI PRO PRÓXIMO DESTINO E VAI OLHAR PRA ELE ANTES DE IR, MAS AI ELE VAI COM UMA VELOCIDADE DAORA E COM O DELTATIME PRA NÃO ZUAR O TEMPO.
        if (currentWP < g.getPathLength())
        {
            goal = g.getPathPoint(currentWP).transform;
            Vector3 lookAtGoal = new Vector3(goal.position.x, this.transform.position.y, goal.position.z);
            Vector3 direction = lookAtGoal - this.transform.position;
            this.transform.rotation = Quaternion.Slerp(this.transform.rotation, Quaternion.LookRotation(direction), Time.deltaTime * rotSpeed);
            this.transform.position = Vector3.MoveTowards(transform.position, goal.position, Time.deltaTime * speed);
        }
    }


    // Metodo para navegação até o heliporto no mapa
    public void GoToHeli()
    {
        g.AStar(currentNode, wps[0]);
        currentWP = 0;
    }

    //Metodo para navegação até as ruinas no mapa
    public void GoToRuin()
    {
        g.AStar(currentNode, wps[7]);
        currentWP = 0;
    }

    //Método para navegação até as indústrias no mapa
    public void GotoIndustry()
    {
        g.AStar(currentNode, wps[9]);
        currentWP = 0;
    }

}
