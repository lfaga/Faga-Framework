//Extensiones de formateo para objetos intrínsecos.

	// Number *********************************
	
	Number.prototype.IntegerDigits = function(digits)
	{
		var sn = this.toString();
		
		if (sn.length < digits)
		{
			for (var i=0; i<(digits-sn.length); i++)
			{
				sn = "0" + sn;
			}
		}
		
		return (sn);
	}
