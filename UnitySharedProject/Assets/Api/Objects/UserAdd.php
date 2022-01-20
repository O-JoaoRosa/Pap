<?php // Executa o Insert do User na BD

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
		// Este script deve receber um url com 3 keyValuePair, a partir do método
		// sendPostRequest() emitido pelo Android.
		$Name = $_POST['UserName'];
		$Password = $_POST['Password'];
		$Email = $_POST['Email'];
		$Image = $_POST['Image'];
		$UserCarIdSelected = intval($_POST['UserCarIDSelected']);
		$Money = intval($_POST['Money']);
		$LastTimeOnline = $_POST['LastTimeOnline'];
		$LastTimeOnline = date("Y-m-d", strtotime($LastTimeOnline));	// dateTime para mySQL tem que ser formatada
		$Reputation = $_POST['Reputation'];


		if($debug_On) echo "DEBUG: Dados \n UserName='$Name'\n \n Password='$Password' \n Email='$Email' \n Image=$Image \n Money=$Money \n Reputation='$Reputation' \n LastTimeOnline='$LastTimeOnline'";

		// Construção da DML Insert com os dados recebidos do Android
		$sql = "Insert INTO `User` (UserName, Password, Email, Image, Money, Reputation, LastTimeOnline, UserCarIDSelected) Values ('$Name',sha2('$Password',512),'$Email',$Image,'$Money','$Reputation','$LastTimeOnline')";

		if($debug_On) echo " \nDEBUG: QUERY:".$sql ."\n";

		//respose da execução da query
		$dbmsResponse = mysqli_query($con,$sql);

		//obtém número de linhas da tabela
		$recordsFound = intval(mysqli_affected_rows($con));

		if($debug_On) echo "\n DEBUG: INSERT: Executei a query. Vou verificar se há dados devolvidos";

		// Se houver dados relacionados=> extrai e analisa de é 0 ou N.
		if ($dbmsResponse)
		{
			if($debug_On) echo "\n DEBUG: INSERT: Há dados. vou extrair a contagem";

			if($debug_On) echo "\n DEBUG: INSERT: recordsFound: ". intval($recordsFound);

			// Se >0 registos => flag passa a false. Não pode eliminar.
			if ($recordsFound > 0)
			{
				if($debug_On) echo "\n DEBUG: INSERT: Inserido com sucesso.";
				echo "DBMS OK: \nRegisto Inserido com Sucesso!";

			} else {
				if($debug_On) echo "\n DEBUG Erro: Inserção com erros, valide os dados.";
				echo "DBMS Erro: \nRegisto Não Inserido.";
			}

			// preparar para as próximas tabelas
			mysqli_free_result($dbmsResponse);
			$recordsFound = -1;
		}
		else{
			echo "\n DBMS indisponível: ".$dbmsResponse;
		}

		//Fecha a ligação
		mysqli_close($con);
	}
?>

