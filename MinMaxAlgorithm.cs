using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinMaxAlgorithm : MonoBehaviour {

    // Use this for initialization
    public Material xmat;
    public Material Omat;
    public List<GameObject> plann = new List<GameObject>();
    public List<int> ttt = new List<int>();
    public int mb;
    int def = 9;
    Node nnn;

    int[,] start = new int[,] { { 0, 0, 0 }, { 0, 0, 0 }, { 0, 0, 0 } };
    Node n;
    void Start () {
       n = new Node(start);
    }

    // Update is called once per frame
    void Update()
    {

        //GreatPlan(n);
        if (def % 2 == 0)
        {
            //def--;
            mb = mimx(n, def, -30, 30, false);
            def--;
            n = nnn;
            GreatPlan(n);
        }
        ////////
        mousput();
        ///

    }

    public int mimx(Node node, int depth,int alpha,int beta, bool ismax)
    {
        
        if (depth == 0 || node.testFinalNode())
        {
            node.test();
            return node.getherstic();

        }

        else
        {
            //List<Node> childs = new List<Node>();
            if (ismax)
            {
                int bestValue = -30;// the infi
                List<Node> childs1 = new List<Node>();
                //for (int i = 0; i < depth; i++)
                node.greatchild(ismax);
                childs1 = node.childs;

                foreach (Node child in childs1)
                {
                    int v = mimx(child, depth - 1, alpha, beta, false);
                    bestValue = Mathf.Max(v, bestValue);
                    alpha = Mathf.Max(alpha, bestValue);
                    if (beta <= alpha)
                        break;

                }
                node.herstic = bestValue;
                return bestValue;
            }
            else
            {
                int bestValue = +30;// the infi
                List<Node> childs = new List<Node>();
                //for (int i = 1; i < depth; i++)
                node.greatchild(ismax);
                childs = node.childs;
                foreach (Node child in childs)
                {
                    int v = mimx(child, depth - 1 , alpha , beta, true);
                    bestValue = Mathf.Min(v, bestValue);
                    beta = Mathf.Min(beta, bestValue);
                    if (beta <= alpha)
                        break;
                }
                node.herstic = bestValue;
                

                int h = childs[0].getherstic();
                int a = 0;
                for (int i = 1; i < childs.Count; i++)
                {
                    if (h > childs[i].getherstic())
                    {
                        a = i;
                        h= childs[i].getherstic();
                    }
                }
                nnn = childs[a];

                    return bestValue;
            }

        }
        
        }

    public void GreatPlan(Node nod)
    {
        int t = 0;
        for(int i=0;i<3; i++)
            for (int j = 0; j < 3;j++)
            {
                if(nod.plan[i,j] == 1)
                    plann[t].GetComponent<MeshRenderer>().materials[0].mainTexture = xmat.mainTexture;
                else if(nod.plan[i, j] == 2)
                    plann[t].GetComponent<MeshRenderer>().materials[0].mainTexture = Omat.mainTexture;
                t++;
            }
    }

    public void GreatNode()
    {

        //int[,] pll = new int[,] { { 0, 0, 0 }, { 0, 0, 0 }, { 0, 0, 0 } };
        //Node nod = new Node(pll);
        int t = 0;
        for (int i = 0; i < 3; i++)
            for (int j = 0; j < 3; j++)
            {
                if (plann[t].GetComponent<MeshRenderer>().materials[0].mainTexture == xmat.mainTexture)
                    n.plan[i, j] = 1;
                else if (plann[t].GetComponent<MeshRenderer>().materials[0].mainTexture == Omat.mainTexture)
                    n.plan[i, j] = 2;

                t++;
            }
       // return nod;
    }

    public void mousput()
    {
        
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 100))
            {
                def--;
                MeshRenderer meshr = hit.transform.gameObject.GetComponent<MeshRenderer>();

                //if(hit.transform.gameObject == S00)
                // {
                //     meshr.materials[0].mainTexture = xmat.mainTexture;
                // }
                meshr.materials[0].mainTexture = xmat.mainTexture;
                GameObject g = hit.transform.gameObject;
                //if(g ==)
                ///////

                
                GreatNode();
            }

        }
    }
    
}
