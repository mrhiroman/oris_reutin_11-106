$("header").append("<div class='glitch-window'></div>");
//fill div with clone of real header
$( "div.header__logo" ).clone().appendTo( ".glitch-window" );