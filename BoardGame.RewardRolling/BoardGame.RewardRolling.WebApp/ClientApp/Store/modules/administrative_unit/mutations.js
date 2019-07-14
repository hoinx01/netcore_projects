export default {
    setListCity:(state, cities) => {
        state.cities = cities;
    },
    setDistrictForCity: (state, cityObj) => {
        var city = state.cities.filter(f => f.id == cityObj.id)[0];
        city.districts = cityObj.districts;
    }
}