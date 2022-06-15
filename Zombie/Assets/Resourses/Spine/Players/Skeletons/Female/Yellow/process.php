<?php

function dealWithNode(&$json, $k, $key)
{
    echo "$k, $key, ".count($json['animations'][$k]['slots'][$key])."\n";
    if (isset($json['animations'][$k]['slots'][$key]['attachment']))
    {
        echo "[here]\n";
        if (count($json['animations'][$k]['slots'][$key]) == 1)
        {
            unset($json['animations'][$k]['slots'][$key]);
        }
        else
        {
            unset($json['animations'][$k]['slots'][$key]['attachment']);
        }
    }
}

$ary = array('face', 'hair_front', 'hair_back', 'hat', 'tachi', 'dagger', 'weapon_hand', 'weapon_back', 'lglove', 'rglove');
$arySkeleton = array('head', 'hip', 'body', 'neck', 'larm_up', 'larm_down', 'rarm_up', 'rarm_down', 'lleg_up', 'lleg_down', 'rleg_up', 'rleg_down', 'lhand', 'rhand', 'lshoot', 'rshoot');

global $json;
$json=file_get_contents("./skeleton.json");
$json=json_decode($json,true);
foreach ($json['animations'] as $k => $v)
{
    foreach ($v['slots'] as $key => $value) {
        /*
        if  (in_array($key, $ary) || strpos($key, '_clothes') !== FALSE)
        {
            dealWithNode($json, $k, $key);
        }
        */
        if (strpos($key, 'effect') === FALSE)
        {
            if (isset($json['animations'][$k]['slots'][$key]['attachment']))
            {
                if (count($json['animations'][$k]['slots'][$key]) == 1)
                {
                    unset($json['animations'][$k]['slots'][$key]);
                }
                else
                {
                    unset($json['animations'][$k]['slots'][$key]['attachment']);
                }
            }
            else
            {

            }
            
        }
    }

    /*
    unset($v['slots']['weapon_hand']);
    unset($v['slots']['weapon_back']);
    unset($v['slots']['hat']);
    unset($v['slots']['face']);
    unset($v['slots']['hair_front']);
    unset($v['slots']['hair_back']);
    

    $k_ary = array();
    foreach ($v['slots'] as $k=>$v)
    {

        if (strpos($k, "_clothes") !== FALSE)
        {
            $k_ary[]=$k;
        }
    }

    foreach ($k_ary as $k)
    {
        unset($v['slots'][$k]);
    }
    */
}

file_put_contents("output.json", json_encode($json));