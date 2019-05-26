Twitter Integration

HTML:
<div id="twitter-timeline" width="40%" height="500"></div>	

JS:
<script>
	window.twttr = (function(d, s, id) {
		var js, fjs = d.getElementsByTagName(s)[0],
		t = window.twttr || {};
		if (d.getElementById(id)) return;
		js = d.createElement(s);
		js.id = id;
		js.src = "https://platform.twitter.com/widgets.js";
		fjs.parentNode.insertBefore(js, fjs);
		 
		t._e = [];
		t.ready = function(f) {
		t._e.push(f);
		};
		 
		return t;
	}(document, "script", "twitter-wjs"));
		 
	twttr.ready(function (twttr) {
		
		twttr.widgets.createTimeline(
				"695909162241368064",
				document.getElementById("twitter-timeline"),
				{
					height: 400
				}
			);
	});
</script>