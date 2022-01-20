<?php // Executa o Update do User na BD

	// Extrai o método de comunicação.
	// É apenas mais um keyValuePair que vem na comunicação e que nos permite
	// destinguir o que fazer a seguir.
	if($_SERVER['REQUEST_METHOD']=='POST'){

		// Abre a ligação
		require_once('../dbConnect.php');

        // Ativa/destaiva as mensagens de debug para postman
        $debug_On = false;

        // controla o nº registos devolvidos. 0 pode elin«minar. Maior que zero,não pode
        $recordsFound = -1;

		// Extrai os dados, a partir de 4 keyValuePairs do url
        $ID = $_POST['ID'];
        $Name = $_POST['Name'];
        $Nickname = $_POST['Nickname'];
        $Email = $_POST['Email'];
        $Gender = intval($_POST['Gender']);
        $BornDate = $_POST['BornDate'];
        $BornDate = date("Y-m-d", strtotime($BornDate));	// dateTime para mySQL tem que ser formatada
        $Password = $_POST['Password'];
        $UserPicURL = $_POST['UserPicURL'];

		// Construção da DML Update com os dados recebidos do Android
		$sql = "UPDATE `User` SET Name='$Name', Nickname='$Nickname', Email='$Email', Gender=$Gender, BornDate='$BornDate', Password='$Password', UserPicURL='$UserPicURL' WHERE ID = $ID;";

        //Executa a query e guarda o resultado.
        $dbmsResponse = mysqli_query($con,$sql);

        //obtém número de linhas da tabela
        $recordsFound = intval(mysqli_affected_rows($con));

        // Se houver dados relacionados=> extrai e analisa de é 0 ou N.
        if ($dbmsResponse) {
            if ($debug_On) echo "\n DEBUG: GET: Há dados. vou extrair a contagem";

            // Se >0 registos => flag passa a false. Não pode eliminar.
            if ($recordsFound > 0) {
                if ($debug_On) echo "\n DEBUG: UPDATE Registo de Nº '$ID' alterado com sucesso";

                echo "Registo alterado com sucesso.";
            } else {
                if ($debug_On) echo "\n DEBUG Erro: UPDATE Registo de Nº '$ID' não foi alterado \n  causa: ". mysqli_error($con);

                echo "Registo não alterado. ";
            }
        }

		//fecha ligação
		mysqli_close($con);
	}
?>