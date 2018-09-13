/*! ========================================================================
 * dashboard-v2.js
 * Page/renders: index.html
 * Plugins used: flot, sparkline, owl carousel
 * ======================================================================== */
$(function () {
    // Stats Carousel
    // ================================
    (function () {
        $("#stats").owlCarousel({
            items: 4
        });
    })();

    // Stats
    // ================================
    (function () {
        // default options
        var option = {
            series: {
                lines: { show: false },
                splines: {
                    show: true,
                    tension: 0.4,
                    lineWidth: 2,
                    fill: 0.5
                },
                points: {
                    show: true,
                    radius: 4
                }
            },
            grid: {
                borderColor: "#eee",
                borderWidth: 1,
                hoverable: true,
                backgroundColor: "#fcfcfc"
            },
            tooltip: true,
            tooltipOpts: { content: "%x : %y" },
            xaxis: {
                tickColor: "#fcfcfc",
                mode: "categories"
            },
            yaxis: { tickColor: "#eee" },
            shadowSize: 0
        }

        // Stats #1
        $.plot("#stats1", [{
            color: "#DC554F",
            data: [ ["Mon", 5], ["Tue", 8], ["Wed", 15], ["Thu", 6], ["Fri", 10] ]
        }], option);

        // Stats #2
        $.plot("#stats2", [{
            color: "#3b5998",
            data: [ ["Mon", 6], ["Tue", 3], ["Wed", 16], ["Thu", 10], ["Fri", 6] ]
        }], option);

        // Stats #3
        $.plot("#stats3", [{
            color: "#6BCCB4",
            data: [ ["Mon", 16], ["Tue", 8], ["Wed", 6], ["Thu", 2], ["Fri", 4] ]
        }], option);

        // Stats #4
        $.plot("#stats4", [{
            color: "#00B1E1",
            data: [ ["Mon", 0], ["Tue", 16], ["Wed", 8], ["Thu", 6], ["Fri", 12] ]
        }], option);

        // Stats #5
        $.plot("#stats5", [{
            color: "#FFD66A",
            data: [ ["Mon", 2], ["Tue", 2], ["Wed", 4], ["Thu", 10], ["Fri", 16] ]
        }], option);
    })();

    // Column filtering
    // ================================
    (function () {
        var $table = $("table#order-list"),
            oTable = $table.dataTable({
            "aoColumns": [
                { "bSortable": false },
                null,
                null,
                null,
                null,
                null,
                null,
                null
            ],
            "oLanguage": {
                "sSearch": "Search all columns:"
            }
        });

        $table.on("keyup", "input[type=search]", function () {
            /* Filter on the column (the index) of this element */
            oTable.fnFilter(this.value, $("tfoot input").index(this));
        });
    })();
});