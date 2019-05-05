import { jsonUrls } from './UrlConstants'
import baseRepository from './BaseRepository'

const speciesRepository = {
    async filter(filterRequest) {
        try {
            var filterResult = await baseRepository.get(jsonUrls.species.filter, { params: filterRequest });
            return Promise.resolve(filterResult);
        }
        catch (exception) {
            return Promise.reject(exception);
        }
    },
    async add(species) {
        try {
            var addResult = await baseRepository.post(jsonUrls.species.add, species);
            return Promise.resolve(addResult);
        }
        catch (exception) {
            return Promise.reject(exception);
        }
    },
    async getById(id) {
        try {
            var result = await baseRepository.post(jsonUrls.species.getById.formatUnicorn({id: id}));
            return Promise.resolve(result);
        }
        catch (exception) {
            return Promise.reject(exception);
        }
    },
}

export default speciesRepository;