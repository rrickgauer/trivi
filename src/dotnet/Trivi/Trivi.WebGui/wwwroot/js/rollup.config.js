/************************************************************************** 

This is the Rollup.JS configuration file.

To get the typescript plugin going:

    cd into the wwwroot/js directory where this file is:

    npm install tslib --save-dev
    npm install typescript --save-dev
    npm install @rollup/plugin-typescript --save-dev
    npm install @types/bootstrap --save-dev 
    npm install nanoid@3 --save-dev
    npm install @rollup/plugin-node-resolve --save-dev 
    npm install @microsoft/signalr --save-dev 
    

***************************************************************************/

import typescript from '@rollup/plugin-typescript';
import resolve from '@rollup/plugin-node-resolve';


const inputPrefix = 'ts/custom/pages/';
const outputPrefix = 'dist/';
const outputSuffix = '.bundle.js';

class RollupConfig
{
    constructor(input, output) {

        this.input = `${inputPrefix}${input}`;


        this.external = [
            'bootstrap',
            //'signalr',
        ];

        this.output = {
            // format: 'es',
            compact: true,
            sourcemap: true,
            file: `${outputPrefix}${output}${outputSuffix}`,
            inlineDynamicImports: true,
            interop: "auto",
            format: 'iife',

            globals: {
                bootstrap: 'bootstrap',
                //signalr: 'signalr',
            },
        }

        this.plugins = [
            typescript(),
            resolve({
                browser: true,
            }),
        ];

    }
}



const configs = [

    new RollupConfig('landing/index.ts', 'landing'),

    new RollupConfig('home/index.ts', 'home'),

    new RollupConfig('collections/index.ts', 'collections'),

    new RollupConfig('auth/login/index.ts', 'login'),
    new RollupConfig('auth/signup/index.ts', 'signup'),

    new RollupConfig('collection/settings/index.ts', 'collection-settings'),
    new RollupConfig('collection/questions/index.ts', 'collection-questions'),
    new RollupConfig('collection/setup/index.ts', 'collection-setup'),


    new RollupConfig('games/join/index.ts', 'games-join'),
    new RollupConfig('games/game/lobby/index.ts', 'game-lobby'),

    new RollupConfig('games/game/questions/short-answer/index.ts', 'game-question-short-answer'),
    new RollupConfig('games/game/questions/true-false/index.ts', 'game-question-true-false'),

    new RollupConfig('games/admin/lobby/index.ts', 'admin-lobby'),

];



// rollup.config.js
export default configs;