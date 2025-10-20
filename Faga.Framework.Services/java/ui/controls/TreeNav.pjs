//Class TreeNav


	// Constructor *********************************
	
	TreeNav = function(_id)
	{
		this.setControlHandle(_id);
	}
	
	
	// Inheritance *********************************
	
	TreeNav.prototype = new Control();


	// Properties **********************************
	
		TreeNav.prototype.isNavOpened = true;
		TreeNav.prototype.navTreeContainer = null;
		TreeNav.prototype.lnkNavTreeToggle = null;
		TreeNav.prototype.imgNavTreeWidget = null;
	

	// Events **************************************
	
	//TreeNav.prototype.onEvent = function(source, eventargs) {}
	
	
	// Methods *************************************
	
	TreeNav.prototype.onLoad = function()
	{
		this.navTreeContainer = document.getElementById(this.id + "_navTreeContainer");
		this.lnkNavTreeToggle = document.getElementById(this.id + "_lnkNavTreeToggle");
		this.imgNavTreeWidget = document.getElementById(this.id + "_imgNavTreeWidget");
		
		this.lnkNavTreeToggle.onclick = this.getlnkNavTreeToggle_OnClick();
	}

	TreeNav.prototype.dispose = function()
	{
		this.navTreeContainer = null;
		this.lnkNavTreeToggle = null;
		this.imgNavTreeWidget = null;
	}
	
	TreeNav.prototype.getlnkNavTreeToggle_OnClick = function()
	{
		var parentInstance = this;
		
		return (
			function()
			{
				parentInstance.ToggleTreeState();
			}
		);
	}
	
	
	TreeNav.prototype.ToggleTreeState = function()
	{
		if (this.isNavOpened)
			this.TreeClose();
		else
			this.TreeOpen();
	}

	TreeNav.prototype.TreeClose = function()
	{
		this.navTreeContainer.style.visibility = "hidden";
		this.navTreeContainer.style.display = "none";
		this.navTreeContainer.style.posWidth = 1;
		
		this.imgNavTreeWidget.src = "images/icons/button_expand.gif";
		
		this.isNavOpened = false;
	}
	
	TreeNav.prototype.TreeOpen = function()
	{
		this.navTreeContainer.style.visibility = "visible";
		this.navTreeContainer.style.display = "block";
		this.navTreeContainer.style.posWidth = 240;
		
		this.imgNavTreeWidget.src = "images/icons/button_contract.gif";
		
		this.isNavOpened = true;
	}
	
	TreeNav.prototype.ToggleFolder = function(strFld)
	{
		var imgW = document.getElementById(this.id + "_imgW" + strFld);
		var imgF = document.getElementById(this.id + "_imgF" + strFld);
		var divCont = document.getElementById(this.id + "_" + strFld);
		
		if (divCont.style.visibility == "hidden")
		{
			imgW.src = "images/icons/tree_button_contract.gif";
			imgF.src = "images/icons/icon_folder_opened.gif";
			divCont.style.visibility = "visible";
			divCont.style.display = "block";
		}
		else
		{
			imgW.src = "images/icons/tree_button_expand.gif";
			imgF.src = "images/icons/icon_folder_closed.gif";
			divCont.style.visibility = "hidden";
			divCont.style.display = "none";
		}
	}
	
	TreeNav.prototype.ItemOnClick = function()
	{
		try { 
			if (this.page.OperationsPending)
			{
			 if (! window.confirm('¿Desea cancelar las operaciones pendientes?'))
				return (false);
				
				this.page.OperationsPending = false;
			}
		}
		catch (e) {}

		try { 
			this.TreeClose();
		}
		catch (e) {}
				
		return (true);
	}
	
//End Class TreeNav