
import { factory } from "./ConfigLog4j";
const logger = factory.getLogger("index");


(async () => {
    try {
        logger.info('Start');
        const t0 = performance.now();
        
        const t1 = performance.now();
        logger.info(`Lapsed:${(t1 - t0).toLocaleString()}ms)`)
    } catch (e) {
        logger.info(e);
    }
})();