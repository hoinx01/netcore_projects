export default {
    filter: {
        request: {
            page: 1,
            limit: 20,
            createdAtMin: new Date(),
            createdAtMax: new Date()
        },
        result: {
            customers: [],
            pagination: {
                count: 0,
                page: 1,
                limit: 20
            }
        }
    }
}
