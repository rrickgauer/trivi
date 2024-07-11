/************************************************************************** 

This is the Rollup.JS configuration file.

To get the typescript plugin going:

    cd into the wwwroot/js directory where this file is:

    npm install tslib --save-dev
    npm install typescript --save-dev
    npm install @rollup/plugin-typescript --save-dev
    npm install @types/bootstrap --save-dev 

***************************************************************************/

import typescript from '@rollup/plugin-typescript';

class RollupConfig
{
    constructor(input, output) {
        this.input = input;

        this.external = ['bootstrap'];

        this.output = {
            // format: 'es',
            compact: true,
            sourcemap: true,
            file: output,
            inlineDynamicImports: true,
            interop: "auto",
            format: 'iife',

            globals: {
                bootstrap: 'bootstrap',
            },
        }

        this.plugins = [typescript()];

    }
}



const configs = [

    new RollupConfig('ts/custom/pages/landing/index.ts', 'dist/landing.bundle.js'),
    new RollupConfig('ts/custom/pages/auth/login/index.ts', 'dist/login.bundle.js'),

];



// rollup.config.js
export default configs;