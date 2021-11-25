using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.Text;

public class Floor : MonoBehaviour {
    private byte[,] map;
    private int max_X;
    private int max_Y;
    public Floor() : this(100) {
        
    }

    public Floor(int x) : this(x, x){

    }

    public Floor(int x, int y) {
        max_X = x;
        max_Y = y;
        map = new byte[max_X, max_Y];


        for (int i = 0; i < max_X; i++) {
            for (int j = 0; j < max_Y; j++) {
                map[i, j] = 1;
            }
        }
        Debug.Log("make Floor");
    }

    public string Debug_print() {
        StringBuilder sb_map = new StringBuilder();
        for(int i = 0; i < max_X; i++) {
            for(int j = 0; j < max_Y; j++) {
                if (map[i,j] == 1) {
                    sb_map.Append("+");
                } else {
                    sb_map.Append(" ");
                }
            }
            sb_map.Append("\r\n");
        }
        string str_map = sb_map.ToString();
        return str_map;
    }
}

public class Spread : MonoBehaviour {
    private int x;
    private int y;
    private int w;
    private int h;

    private int deep;

    private bool vertical = true;
    private int m;

    private Room room;

    private Spread A;
    private Spread B;

    private int Maximum_size = 40;
    private int Minimum_size = 5;
    private int Max_deeps = 1;


    public Spread(int aw, int ah) : this(0, 0, aw, ah, 0) {
        
    }

    public Spread(int ax, int ay, int aw, int ah, int ad) {
        x = ax;
        y = ay;
        w = aw;
        h = ah;
        deep = ad;
        if (Check_size() && deep < Max_deeps) {
            Cut();
        }else if (true) {
            room = new Room(x,y,w,h);
        }
    }

    private void Cut() {
        if (Random.value < 0.5) {
            vertical = false;
        }

        if (vertical) {
            m = w;
        } else {
            m = h;
        }
        m = (int)(((m * Random.value) + (m * Random.value)) / 2);

        if (vertical) {
            A = new Spread(x, y, m, h, deep - 1);
            B = new Spread(x + m + 1, y, w - m - 1, h, deep - 1);
        } else {
            A = new Spread(x, y, w, m, deep - 1);
            B = new Spread(x, y + m + 1, w, h - m - 1, deep - 1);
        }
    }

    private bool Check_size() {
        if(Minimum_size < w && w < Maximum_size && Minimum_size < h && h < Maximum_size) {
            return true;
        }
        return false;
    }
}

public class Room : MonoBehaviour {
    private int x;
    private int y;
    private int w;
    private int h;
    
    public Room(int ax, int ay, int aw, int ah) {
        w = (int)(aw * Random.value);

        //•”‰®‚ðì‚é‚Æ‚±‚ë‚©‚çD
    }
}