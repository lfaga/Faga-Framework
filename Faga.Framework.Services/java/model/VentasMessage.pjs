//Class VentasMessage

	// Constructor *********************************

	VentasMessage = function()
	{
		this.xmldocument = new ActiveXObject("Msxml2.DOMDocument.4.0");
		this.xmldocument.loadXML("<VentasSetItemsParams>\n</VentasSetItemsParams>");		
	}
	
	
	// Properties **********************************

	VentasMessage.prototype.xmldocument = null;
	
		
	// Methods *************************************

	//USAGE: AddVentaMessage(1, 2, '2006-02-22', '', 100.5, 4, false);
	VentasMessage.prototype.AddVentaMessage =
		function(idcc, fecha, desc, imp, comp, est)
	{
		var el;		
		var root = this.AddElement(
			this.xmldocument.documentElement, "VentaMessage");
		el = this.AddElement(root, "idConceptoContrato");
		el.text = idcc;
		el = this.AddElement(root, "fecha");
		el.text = fecha.getFullYear() + "-" + (fecha.getMonth() + 1).toString() + "-" + fecha.getDate();
		el = this.AddElement(root, "descripcion");
		el.text = desc;
		el = this.AddElement(root, "importe");
		el.text = imp;
		el = this.AddElement(root, "comprobantes");
		el.text = comp;
		el = this.AddElement(root, "estimado");
		el.text = est;
	}

	VentasMessage.prototype.AddElement = function(parentElement, tagName)
	{
		var n = this.xmldocument.createElement(tagName);
		parentElement.appendChild(n);
		return (parentElement.lastChild);
	}
	
	VentasMessage.prototype.toXML = function()
	{
		return ("<?xml version='1.0' encoding='utf-8'?>"
			+ "<soap:Envelope xmlns:xsi='http://www.w3.org/2001/XMLSchema-instance' xmlns:xsd='http://www.w3.org/2001/XMLSchema' xmlns:soap='http://schemas.xmlsoap.org/soap/envelope/'>"
			+ "<soap:Body>"
			+ "<VentasSetItems xmlns='http://registroventas.irsa.com.ar/wal/'>"
			+ this.xmldocument.xml
			+ "</VentasSetItems>"
			+ "</soap:Body>"
			+ "</soap:Envelope>"
		);
	}

	VentasMessage.prototype.dispose = function()
	{
		this.xmldocument = null;
	}


//End Class VentasMessage