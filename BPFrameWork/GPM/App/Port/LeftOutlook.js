   function ItemOver(ctrl )
   {
     ctrl.style.backgroundColor='LightBlue';
     // ctl.class='ItemOn';
   }
   function ItemOut(ctrl)
   {
     ctrl.style.backgroundColor='infobackground';
     // ctl.class='ItemOut';
   }
   
function WinOpen( url, winName)
{
      newWindow=window.open( url , winName, 'width=700,top=100,left=200,height=400,scrollbars=yes,resizable=yes,toolbar=false,location=false');
      newWindow.focus();
}
 
