<?php // obtem todos os registos da tabela User da BD para a ListView do Mobile


	// Abre a Ligação
	require_once('../dbConnect.php');

	// Ativa/destaiva as mensagens de debug para postman
	$debug_On = false;

	// controla o nº registos devolvidos. 0 pode elin«minar. Maior que zero,não pode
	$recordsFound = -1;

	// Prepara e executa a query para recolha de todos os registos da tabela User
	$sql = "SELECT * FROM `Car` order by ID asc";

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

				array_push($result,array(
					"ID"=>$row['ID'],
					"Descri"=>$row['Descri'],
					"ReputationRequired"=>$row['ReputationRequired'],
					"Price"=>$row['Price'],
					"FowardSpeed"=>$row['FowardSpeed'],
					"DriftTurnAngle"=>$row['DriftTurnAngle'],
					"DefaultTurnAngle"=>$row['DefaultTurnAngle'],
					"GroundDrag"=>$row['GroundDrag'],
					"AirDrag"=>$row['AirDrag'],
				));

				// NOTA: Atributos são case sensitive.
			}

			echo json_encode($result);
		}
	}

	// fecha a ligação
	mysqli_close($con);
?>	