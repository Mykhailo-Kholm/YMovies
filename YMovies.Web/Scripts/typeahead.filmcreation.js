$(document).ready(function () {

	var i = 0;

	var cast = new Bloodhound({
		datumTokenizer: Bloodhound.tokenizers.obcountryCount.whitespace('name'),
		queryTokenizer: Bloodhound.tokenizers.whitespace,
		remote: {
			url: '/api/actors?query=%QUERY',
			wildcard: '%QUERY'
		}
	});

	$('#actor').typeahead({
		minLength: 2,
		highLight: true,
	}, {
		name: 'cast',
		display: 'name',
		source: cast
	}).on("typeahead:select", function (e, actor) {

		console.log(i);
		$("#actors").append(
			'<input type="hidden" id="CastID[' + i + ']" name="Cast[' + i + '].id" value=""/>' +
			'<input type="text" id=Cast[' + i + '] name="Cast[' + i + '].name" value=""/>');

		var inputName = document.getElementById('Cast[' + i + ']')
		var inputID = document.getElementById('CastID[' + i + ']')
		inputName.value = actor.name
		inputID.value = actor.id
		$("#actor").typeahead('val', '');
		i += 1;
	});

	var countryCount = 0;

	var country = new Bloodhound({
		datumTokenizer: Bloodhound.tokenizers.obj.whitespace('name'),
		queryTokenizer: Bloodhound.tokenizers.whitespace,
		remote: {
			url: '/api/countries?query=%QUERY',
			wildcard: '%QUERY'
		}
	});

	$('#countryInput').typeahead({
		minLength: 2,
		highLight: true,
	}, {
		name: 'country',
		display: 'name',
		source: country
	}).on("typeahead:select", function (e, country) {

		console.log(j)
		$("#countries").append(
			'<input type="hidden" id="CountryID[' + countryCount + ']" name="Country[' + countryCount + '].id" value=""/>' +
			'<input type="text" id="Country[' + countryCount + ']" name="Country[' + countryCount + '].name" value=""/>');

		var countryName = document.getElementById("Country[" + countryCount + "]");
		var countryID = document.getElementById('CountryID[' + countryCount + ']');

		countryName.value = country.name;
		countryID.value = country.id;
		$("#countryInput").typeahead('val', '');
		countryCount++;
	});

	var genresCount = 0;

	var genre = new Bloodhound({
		datumTokenizer: Bloodhound.tokenizers.obj.whitespace('name'),
		queryTokenizer: Bloodhound.tokenizers.whitespace,
		remote: {
			url: '/api/genres?query=%QUERY',
			wildcard: '%QUERY'
		}
	});

	$('#genreInput').typeahead({
		minLength: 2,
		highLight: true,
	}, {
		name: 'gerne',
		display: 'name',
		source: genre
	}).on("typeahead:select", function (e, country) {

		$("#genres").append(
			'<input type="hidden" id="GenreID[' + genresCount + ']" name="Genre[' + genresCount + '].id" value=""/>' +
			'<input type="text" id="Genre[' + genresCount + ']" name="Genre[' + genresCount + '].name" value=""/>');

		var countryName = document.getElementById("Genre[" + genresCount + "]");
		var countryID = document.getElementById('GenreID[' + genresCount + ']');

		countryName.value = country.name;
		countryID.value = country.id;
		$("#genreInput").typeahead('val', '');
		genresCount++;
	});

});