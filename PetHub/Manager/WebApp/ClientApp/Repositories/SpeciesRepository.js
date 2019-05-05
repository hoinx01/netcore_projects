import { jsonUrls } from './UrlConstants'
import baseRepository from './BaseRepository'

const speciesRepository = {
    async filter(filterRequest) {
        try {
            var filterResult = await baseRepository.get(jsonUrls.filterSpecies, { params: filterRequest});
            return Promise.resolve(filterResult);
        }
        catch (exception) {
            return Promise.reject(exception);
        }
    },
    async add(species) {
        try {
            var addResult = await baseRepository.post(jsonUrls.adddSpecies, species);
            return Promise.resolve(addResult);
        }
        catch (exception) {
            return Promise.reject(exception);
        }
    }
}

export default speciesRepository;