 
 
function WinOpen(url, winName)
{
            newWindow=window.open( url , winName ,'width=600,top=200,left=150,height=400,scrollbars=yes,resizable=yes,toolbar=false,location=false');
            newWindow.focus();  
}
//true:当前处于扩展状态，灰色；false:当前处于缩小状态，蓝色
//当窗体加载时，应为缩小状态
var  g_Extended=true;
//记录Frame的大小。
//var Top_Rows='75,*';
var Top_Rows='90,*';
//var Bottom_Cols='198,*';
var Bottom_Cols='100,*';
function shrinkForm()
{        
	window.parent.Top.rows=Top_Rows;
	window.parent.Bottom.cols=Bottom_Cols;
	switchUpPoint.innerText=5
	switchLeftPoint.innerText=3
}
function ExtendForm()
{
	//此段代码无法实现预定的功能。当窗口的帧的大小改变时，
	//帧的rows和cols属性不会马上改变。
	if(window.parent.Top.rows!='0,*') Top_Rows=window.parent.Top.rows;
	if(window.parent.Bottom.cols!='0,*') Bottom_Cols=window.parent.Bottom.cols;
	switchUpPoint.innerText=6
	switchLeftPoint.innerText=4
	window.parent.Top.rows='0,*';
	window.parent.Bottom.cols='0,*';
}

function switchUpBar(){
	if (switchUpPoint.innerText==5){
		switchUpPoint.innerText=6
		Top_Rows=window.parent.Top.rows;
		window.parent.Top.rows='0,*';
	}
	else{
		window.parent.Top.rows=Top_Rows;
		switchUpPoint.innerText=5
	}
	if(window.parent.Bottom.cols=='0,*' && window.parent.Top.rows=='0,*') 
		{
		document.all("Tip").innerText="标准化";
		document.all("imgsrc").src="max.gif";
		}
	else
		{
		document.all("Tip").innerText="最大化";
		document.all("imgsrc").src="mix.gif";
	    }
}

function switchLeftBar(){
	if (switchLeftPoint.innerText==3){
		switchLeftPoint.innerText=4
		Bottom_Cols=window.parent.Bottom.cols;

		window.parent.Bottom.cols='0,*';
	}
	else{
		window.parent.Bottom.cols=Bottom_Cols;
		switchLeftPoint.innerText=3
	}

	if(window.parent.Bottom.cols=='0,*' && window.parent.Top.rows=='0,*') 
		{
		document.all("Tip").innerText="标准化"
		document.all("imgsrc").src="max.gif";
		}
	else
		{
		document.all("Tip").innerText="最大化"
		document.all("imgsrc").src="mix.gif";
		}

}
function ShiftStatus()
{
//当窗体加载时，应为缩小状态
//当前状态为扩展状态，要切换到缩小状态。
  if (g_Extended==true)
  {
	document.all("Tip").innerText="最大化"
	document.all("imgsrc").src="mix.gif";
	shrinkForm()
   }
   else
   {
	document.all("Tip").innerText="标准化"
	document.all("imgsrc").src="max.gif";
	ExtendForm();
   }
   
   g_Extended=!g_Extended;
   
}
 