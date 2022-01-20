<?php // obtem todos os registos da tabela User da BD para a ListView do Mobile
	if ($_SERVER['REQUEST_METHOD'] == "GET") {

		// Abre a Ligação
		require_once('../dbConnect.php');

		// Ativa/destaiva as mensagens de debug para postman
		$debug_On = false;

		// controla o nº registos devolvidos. 0 pode elin«minar. Maior que zero,não pode
		$recordsFound = -1;

		// Prepara e executa a query para recolha de todos os registos da tabela User
		$sql = "SELECT * FROM `User` order by ID desc";

		//Executa a query e guarda o resultado.
		$dbmsResponse = mysqli_query($con,$sql);

		//obtém número de linhas da tabela
		$recordsFound = intval(mysqli_affected_rows($con));

		// Se houver dados relacionados=> extrai e analisa de é 0 ou N.
		if ($dbmsResponse) {
			if ($debug_On) echo "\n DEBUG: GET: Há dados. vou extrair a contagem";

			// Se >0 registos => flag passa a false. Não pode eliminar.
			if ($recordsFound > 0) {
				if($debug_On) echo "\n DEBUG: GET: Registo obtido com sucesso.";

				// Array result para receber os registos
				$result = array();

				// Enquanto houver registos para ler, extrai para a var row e insere-os no array keyValuePair
				while($row = mysqli_fetch_array($dbmsResponse)){
					array_push($result,array(				// Envia para o array result, um novo array com keyValuePairs
						"ID"=>$row['ID'],							// Atributo ID, seguido do seu valor
						"Name"=>$row['Name'],						// Atributo Name, seguido do seu valor
						"Nickname"=>$row['Nickname'],			    // Atributo Nickname, seguido do seu valor
						"Email"=>$row['Email'],			            // Atributo Email, seguido do seu valor
						"Gender"=>$row['Gender'],	                // Atributo Gender, seguido do seu valor
						"BornDate"=>$row['BornDate'],			    // Atributo BornDate, seguido do seu valor
						"Password"=>$row['Password'],			    // Atributo Password, seguido do seu valor
						"UserPicURL"=>$row['UserPicURL'],	        // Atributo UserPicURL, seguido do seu valor
					));
					// NOTA: Atributos são case sensitive.
				}

				// Codifica o array no formato JSON e devolve-a (echo) ao Cliente.
				// No Cliente, o array result é extraído no formato json, seguido
				// da extração do seu conteúdo. O nome do array é muito importante.
				echo json_encode(array('result'=>$result));
			}
		}

		// fecha a ligação
		mysqli_close($con);
	}
?>	