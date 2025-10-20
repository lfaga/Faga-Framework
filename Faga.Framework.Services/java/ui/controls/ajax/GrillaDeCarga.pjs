//Class GrillaDeCarga

	// Constructor *********************************
	
	GrillaDeCarga = function(id)
	{
		this.setControlHandle(id);
		this.Controls = new Dictionary();
		this._loadServiceURL = this._baseServiceURL + "ventas.asmx/VentasGetItems";
		this._saveServiceURL = this._baseServiceURL + "ventas.asmx";
		this._soapAction = "http://registroventas.irsa.com.ar/wal/VentasSetItems";
	}
		
	
	// Inheritance *********************************
	
	GrillaDeCarga.prototype = new AJAXControl();


	// Properties **********************************
	
	GrillaDeCarga.prototype._loadServiceURL = null;
	GrillaDeCarga.prototype._saveServiceURL = null;
	GrillaDeCarga.prototype.idContrato = 1;
	GrillaDeCarga.prototype.fecha = new Date();
	GrillaDeCarga.prototype.tbody = null;
	GrillaDeCarga.prototype.Controls = null;
	
	// Methods *************************************
	
	
	GrillaDeCarga.prototype.onLoad = function()
	{
		this.tbody = this.controlHandle.tBodies[0];
		this.update(null, null);
	}
	
	GrillaDeCarga.prototype.dispose = function()
	{
		this.ClearControls();
		this.tbody = null;
	}
	
	GrillaDeCarga.prototype.ClearControls = function()
	{
		for (var id in this.Controls.Items)
		{
			this.page.Controls.Remove(id);
			this.Controls.Get(id).dispose();
			this.Controls.Get(id).releaseControlHandle();
		}
		this.Controls.Clear();
	}
	
	GrillaDeCarga.prototype.update = function(_idContrato, _date)
	{
		if (_idContrato != null) this.idContrato = _idContrato;
		if (_date != null) this.fecha = _date;

		this.ClearControls();
		
		this.page.StatusBar.ShowMessage("Procesando...");
		this.Execute(this._loadServiceURL,
			"idContrato=" + this.idContrato.toString()
			+ "&fecha="	+ this.fecha.toDateString()
		);
	}
		
	GrillaDeCarga.prototype.ParseResponse = function(_responseXML)
	{
		try
		{
			this.page.StatusBar.Clear();
						
			if (_responseXML.getElementsByTagName("ArrayOfVentaMessage").length > 0)
				this.processVentasGetItemsResponse(_responseXML)
			else if (_responseXML.getElementsByTagName("VentasSetItemsResponse").length > 0)
				this.processVentasSetItemsResponse(_responseXML)
			else
				alert(_responseXML.xml);
		}
		catch (e)
		{
			alert(e.message);
		}
	}
	
	GrillaDeCarga.prototype.processVentasGetItemsResponse = function(rx)
	{
		var vi = rx.getElementsByTagName("ArrayOfVentaMessage")[0];
		
		var tb = this.tbody;
		
		while (tb.rows.length > 0)
			tb.deleteRow();
		
		for (i=0; i<vi.childNodes.length; i++)
		{
			var tr = tb.insertRow();
			var el = vi.childNodes[i];
			var ctl;
			var o;		
			var td;
			var par = (i % 2 == 0);							
			var sIdCC = el.getElementsByTagName("idConceptoContrato")[0].text;
			var sConcepto =  el.getElementsByTagName("descripcion")[0].text;
			
			//Create Cell --------------------------------------------------
			td = tr.insertCell();
			td.className = (par ? "colimpar" : "colimpar2");
			//Create hidCC
			o = document.createElement("INPUT");
			o.id = this.id + "_hidCC_" + sIdCC;
			o.setAttribute("type", "hidden");
			o.value = sIdCC;
			td.appendChild(o);
			//Create label
			o = document.createElement("SPAN");
			o.innerText = sConcepto;
			o.setAttribute("UNSELECTABLE", "ON");
			td.appendChild(o);

			//Create Cell --------------------------------------------------
			td = tr.insertCell();
			td.className = (par ? "colpar" : "colpar2");
			//Create txtImporte
			ctl = this.createNumericBox( td, "_txtImporte_"	+ sIdCC,
				96, "right", el.getElementsByTagName("importe")[0].text,
				true, 2, "Importe de " + sConcepto);
			ctl.validate();

			//Create Cell --------------------------------------------------
			td = tr.insertCell();
			td.className = (par ? "colimpar" : "colimpar2");
			//Create txtCantComp
			ctl = this.createNumericBox( td, "_txtCantComp_"	+ sIdCC,
				96, "right", el.getElementsByTagName("comprobantes")[0].text,
				false, 0, "Cant. Comp. de " + sConcepto);
			ctl.validate();

			//Create Cell --------------------------------------------------
			td = tr.insertCell();
			td.className = (par ? "colpar" : "colpar2");
			//Create chkEstimado
			o = document.createElement("INPUT");
			o.id = this.id + "_chkEstimado_"	+ sIdCC;
			o.setAttribute("type", "checkbox");
			td.appendChild(o);
			o.checked = (el.getElementsByTagName("estimado")[0].text == "True");
		}
	}
	
	GrillaDeCarga.prototype.processVentasSetItemsResponse = function(rx)
	{
		var s = rx.getElementsByTagName("VentasSetItemsResult")[0];
		if (s.text.toLowerCase() == "ok")
			this.page.StatusBar.ShowSuccess("Se grab&oacute; satisfactoriamente.");
		else
			this.page.StatusBar.ShowError(s.text);
	}

	
	GrillaDeCarga.prototype.createNumericBox = function(container, ctlId,
		iwidth, talign, value, isfixed, precision, description)
	{
		var o = document.createElement("INPUT");
		o.id = this.id + ctlId;
		o.className = "box";
		o.style.width = iwidth.toString() + "px";
		o.style.textAlign = talign;
		container.appendChild(o);

		if (value != null)
			o.value = value;
		else
			o.value = "";
		
		var ctl = new NumericBox(o.id, isfixed, precision);
		this.Controls.Add(ctl.id, ctl);
		this.page.addControl(ctl);
		ctl.onLoad();
		ctl.description = description;
		return (ctl);
	}
	
	GrillaDeCarga.prototype.Save = function()
	{
		vm = new VentasMessage();
		for (var x=0; x<this.tbody.rows.length; x++)
		{
			var row = this.tbody.rows[x];
			var vals = new Dictionary();
			
			for (var y=0; y<row.cells.length; y++)
			{
				var cell = row.cells[y];
				var ctl = cell.childNodes[0];
				
				if (ctl.type == "checkbox")
					vals.Add(ctl.id, ctl.checked);
				else
					vals.Add(ctl.id, ctl.value);
			}
			
			vm.AddVentaMessage(
				vals.Get(this.id + "_hidCC_" + (x+1).toString()),
				this.fecha, "",
				vals.Get(this.id + "_txtImporte_" + (x+1).toString()),
				vals.Get(this.id + "_txtCantComp_" + (x+1).toString()),
				vals.Get(this.id + "_chkEstimado_" + (x+1).toString())
			);
		}
		
		this.page.StatusBar.ShowMessage("Procesando...");
		this.ExecuteSOAP(this._saveServiceURL, vm.toXML(), this._soapAction);
	}

	GrillaDeCarga.prototype.toString = function()
	{
		return ("GrillaDeCarga object");
	}
	
	
//End Class GrillaDeCarga