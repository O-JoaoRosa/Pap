<?php

    include_once("dbConnect.php");
    if(isset($_POST["username"]) && !empty($_POST["username"]) &&
        isset($_POST["password"]) && !empty($_POST["password"])){
        Login($_POST["username"], $_POST["password"]);
    }

    function Login($username, $password){
        GLOBAL $con;
        $sql = "SELECT * FROM user WHERE username=? AND password=?";
        $st=$con->prepare($sql);

        $st->execute(array($username,sha2('$Password',512)));
        $all=$st->fetchAll();
        if(count($all) == 1){
            echo "SERVER: ID#".$all[0]["ID"]." - ".$all[0]["UserName"]." - ".$all[0]["Image"]." - ".$all[0]["Money"]." - ".$all[0]["Reputation"]." - ".$all[0]["UserCarIDSelected"];
            exit();
        }

        //caso a palavrapass ou o username forem errados
        echo "SERVER: erro: invalid username or password";
        exit();
    }

    //if username or password are null
    echo "SERVER: erro: enter a valid username & password";
    exit();



?>