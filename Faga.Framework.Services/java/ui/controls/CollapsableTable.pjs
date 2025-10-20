//Class CollapsableTable

	// Constructor *********************************
	
		CollapsableTable = function(_id)
		{
			this.setControlHandle(_id);
			this.imgCollapse = document.getElementById(_id + "_imgCollapse");
		}

	
	// Inheritance *********************************
	
		CollapsableTable.prototype = new Control();


	// Properties **********************************
	
		CollapsableTable.prototype.imgCollapse = null;
		CollapsableTable.prototype.arrayRows = null;
		CollapsableTable.prototype.visible = false;
	
	// Events **************************************

	//	CollapsableTable.prototype.onClick = function(source, eventargs) {}

	// Methods *************************************
	
		CollapsableTable.prototype.onLoad = function()
		{
			this.imgCollapse.onclick = this.getThis_OnClick();
			
			this.arrayRows = this.controlHandle.getElementsByTagName("TR");
			this.setVisibility(this.visible);
		}

		CollapsableTable.prototype.dispose = function()
		{
			this.imgCollapse.onclick = null;
			this.imgCollapse = null;
			this.arrayRows = null;
		}
		
		CollapsableTable.prototype.getThis_OnClick = function()
		{
			var parentInstance = this;
			
			return (
				function()
				{
					parentInstance.setVisibility(! parentInstance.visible);

					return (false);
				}
			);
		}
		

		CollapsableTable.prototype.setVisibility = function(_visible)
		{
			this.visible = _visible;

			this.imgCollapse.src = ( _visible ?
					"/images/icons/icon_tbl_contract.gif" :
					"/images/icons/icon_tbl_expand.gif" );

			for (i=0; i<this.arrayRows.length; i++)
			{
				var o = this.arrayRows[i];
				var sname = o.getAttribute("name");
				if ( (sname != null) && (sname == this.id + "_hiddable") )
				{
					o.style.visibility = ( _visible ? "visible" : "hidden" );
					o.style.display = ( _visible ? "block" :  "none" );
				}
			}
		}
		

//End Class CollapsableTable