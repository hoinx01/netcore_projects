export default {
    format(date, template) {
        let year = date.getFullYear();
        let month = date.getMonth() + 1;
        let day = date.getDate();

        if (month < 10)
            month = "0" + month.toString();
        if (day < 10)
            day = "0" + day.toString();

        let formated = template.replace("YYYY", year.toString());
        formated = formated.replace("MM", month.toString());
        formated = formated.replace("DD", day.toString());
        return formated;
    }
}