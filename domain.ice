/*
 * MIT License
 *
 * Copyright (c) 2020 Javier Zuleta Silva <jzuletas005@gmail.com>.
 *
 * Permission is hereby granted, free of charge, to any person obtaining a copy
 * of this software and associated documentation files (the "Software"), to deal
 * in the Software without restriction, including without limitation the rights
 * to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
 * copies of the Software, and to permit persons to whom the Software is
 * furnished to do so, subject to the following conditions:
 *
 * The above copyright notice and this permission notice shall be included in all
 * copies or substantial portions of the Software.
 *
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
 * IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
 * FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
 * AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
 * LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
 * OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
 * SOFTWARE.
 */

// https://doc.zeroc.com/ice/3.7/language-mappings/java-mapping/client-side-slice-to-java-mapping/customizing-the-java-mapping
["java:package:cl.ucn.disc.pdis.fivet.zeroice", "cs:namespace:Fivet.ZeroIce"]
module model {

    /**
    *Foto
    */
    class Foto{
        /**
        *Nombre/URL: nombre del archivo de imagen o link hacia la foto. ej:nacimientoFirulay.png
        */
        string urlNomb;
    }
    /**
    *Examen
    */
    class Examen{

        /**
        *PK
        */
        int id;

        /**
        *Nombre: nombre del examen. Ej: Radiología
        */
        string nombreExam;

        /**
        *Fecha: fecha en la que fue tomado el examen. ej: 15/06/2015
        *Format: ISO_ZONED_DATE_TIME
        */
        string fechaExam;
    }
    /**
    *Dueño
    */
    class Persona{

        /**
        *PK
        */
        int id;

        /**
        *Nombre: nombre del dueño. ej: Juan
        */
        string nombre;

        /**
        *Dirección: dirección del dueño. ej: Angamos 0610
        */
        string direccion;

        /**
        *Teléfono fijo: número de teléfono fijo. ej: 055 456765
        */
        long fonoFijo;

        /**
        *Teléfono móvil: número de teléfono móvil. ej: 7567328
        */
        long fonoMovil;

        /**
        *Email: dirección de correo electrónico de contacto. ej: contacto@dominio.com
        */
        string email;

        /**
        *RUT: rut del dueño. ej: 12.442.675-1
        */
        string rut;

    }
    /**
    *Control
    */
    class Control{
        /**
        *id key
        */
        int id;

        /**
        *Fecha: fecha del control.
        *Format: ISO_ZONED_DATE_TIME
        */
        string fechaControl;

        /**
        *Fecha próximo control: fecha del próximo control
        *Format: ISO_ZONED_DATE_TIME
        */
        string fechaProx;

        /**
        *Temperatura: del paciente en grados celsius
        */
        double temperatura;

        /**
        *Peso: del paciente expresado en kilos
        */
        double peso;

        /**
        *Altura: del paciente expresado en cm
        */
        double altura;

        /**
        *Diagnóstico: Descripción textual del control realizado.
        */
        string diagnostico;

        /**
        *Veterinario: Nombre del veterinario que realizó el control.
        */
        string veterinario;
    }
    /**
    *Ficha
    */
    class Ficha{

        /**
        *PK
        */
        int id;

        /**
        *Número de ficha: correlativo numérico único asignado automáticamente. ej:20128
        */
        int nFicha;

        /**
        *Nombre: nombre del paciente, ej: Firulay
        */
        string nombre;

        /**
        *Especie: especie del animal, ej: canino
        */
        string especie;

        /**
        *Fecha de Nacimiento: fecha de nacimiento del animal, puede ser estimada,ej: enero 2014.
        */
        string fechaNacimiento;

        /**
        *Raza: del animal, ej: rottweiler
        */
        string raza;

        /**
        *Color: rojo cobrizo
        */
        string color;

        /**
        *Sexo: macho, hembra
        */
        string sexo;

        /**
        *Tipo: de paciente, ej: interno o externo.
        */
        string tipo;

    }
    /**
    *Sexo
    */
    enum Sexo {HEMBRA, MACHO}
    /**
    *Tipo de paciente
    */
    enum Tipo{INTERNO, EXTERNO}

    /**
    * Contratos.
    */
    interface Contratos{
        /**
        *Retorna una ficha segun el numero entregado como parametro
        *@param numero de ficha
        *@return Ficha
        */
        Ficha obtenerFicha(int nFicha);
        /**
        *Agrega un control a la lista control
        *@param control
        */
        Control ingresarControl(Control control);
        /**
        *Agrega un dueño como persona
        *@param persona
        */
        Persona ingresarDueno(Persona persona);
    }
    /**
     * The base system.
     */
     interface TheSystem {

        /**
         * @return the diference in time between client and server.
         */
        long getDelay(long clientTime);

     }

}
