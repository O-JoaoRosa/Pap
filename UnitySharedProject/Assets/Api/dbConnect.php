<?php
        // Este script é executado a partir do inicio dos restantes

        // Credenciais da Base de dados no Server Fantastic DB
        define("DB1_HOST", "cpanel86.dnscpanel.com");
        define("DB1_USER", "fantast4_francisco");
        define("DB1_PASSWORD", "Ribeiro3646");
        define("DB1_DATABASE", "fantast4_francisco");
        define("DB1_PORT", "3306");

        // Credenciais da Base de dados no Server IberWeb
        define("DB2_HOST", "188.93.230.170");
        define("DB2_USER", "vigionpt_vigio_t05jrosa");
        define("DB2_PASSWORD", "Jr2003!;"); 
        define("DB2_DATABASE", "vigionpt_tgpsi05_jrosa");
        define("DB2_PORT", "3306");

        // abertura da ligação (Sintaxe MSQLi)
        // $con = mysqli_connect(DB1_HOST, DB1_USER, DB1_PASSWORD, DB1_DATABASE, DB1_PORT);
        $con = mysqli_connect(DB2_HOST, DB2_USER, DB2_PASSWORD, DB2_DATABASE, DB2_PORT);

        // Define a codificação internacional Unicode.
        $con->set_charset("utf8mb4");
        if (!$con) {						// Faz a ligação ou devolve o erro ao Mobile
            die("CONNECTION FAIL: ".$con->connect_error);
        }

        // O encerramento da ligação é feita nos scripts de DML
?>