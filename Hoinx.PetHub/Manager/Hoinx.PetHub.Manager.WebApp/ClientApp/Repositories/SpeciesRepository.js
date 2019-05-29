import { jsonUrls } from './UrlConstants'
import baseRepository from './BaseRepository'
import String from '../Utils/StringExtensions'

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
    async update(species) {
        try {
           
            var updateResult = await baseRepository.put(String.formatUnicorn(jsonUrls.species.update, { id: species.id }), species);
        }
        catch (exception) {
            return Promise.reject(exception);
        }
    },
    async delete(species) {
        try {
            var deleteResult = await baseRepository.delete(String.formatUnicorn(jsonUrls.species.delete, { id: species.id }));
        }
        catch (exception) {
            return Promise.reject(exception);
        }
    },
    async getById(id) {
        try {
            var result = await baseRepository.get(String.formatUnicorn(jsonUrls.species.getById, {id: id}));
            return Promise.resolve(result);
        }
        catch (exception) {
            return Promise.reject(exception);
        }
    },
}

export default speciesRepository;