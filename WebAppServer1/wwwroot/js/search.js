$(function () {
    $('form').submit(async e => {
        e.preventDefault();
        const q = $('#search').val();

        const r = await fetch('/Reviews/Search2?query=' + q);
        const d = await r.json();
        console.log(d);

        const template = $('#template').html();
        let results = '';
        for (var item in d) {
            let row = template;
            for (var key in d[item]) {
                console.log(key, d[item][key])
                row = row.replaceAll('{' + key + '}', d[item][key]);
                row = row.replaceAll('%7B' + key + '%7D', d[item][key]);

            }
            results += row;
        }

        $('tbody').html(results);

    })

});